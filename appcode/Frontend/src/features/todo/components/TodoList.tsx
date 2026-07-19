import { useEffect, useState } from 'react';
import { createPortal } from 'react-dom';
import { type Todo } from '../models/todo';
import { todoService } from '../services/todoService';
import { TodoDialog } from './TodoDialog';
import { showToast } from '../../../shared/components/Toast';
import './TodoList.css';
import { EllipsisVertical, Circle, CircleCheckBig } from 'lucide-react';

interface MenuState {
  todo: Todo;
  top: number;
  right: number;
}

export function TodoList() {
  const [todos, setTodos] = useState<Todo[]>([]);
  const [searchTerm, setSearchTerm] = useState('');
  const [isLoading, setIsLoading] = useState(true);
  const [updatingIds, setUpdatingIds] = useState<Set<number>>(new Set());
  const [menu, setMenu] = useState<MenuState | null>(null);
  const [dialogState, setDialogState] = useState<'new' | Todo | null>(null);

  useEffect(() => { loadTodos(); }, []);

  function loadTodos() {
    setIsLoading(true);
    return todoService.getAll()
      .then(setTodos)
      .finally(() => setIsLoading(false));
  }

  function markComplete(todo: Todo) {
    setUpdatingIds(prev => new Set(prev).add(todo.id));
    todoService.updateComplete(todo.id)
      .then(loadTodos)
      .finally(() => {
        setUpdatingIds(prev => {
          const next = new Set(prev);
          next.delete(todo.id);
          return next;
        });
      });
  }

  function openMenu(todo: Todo, e: React.MouseEvent<HTMLButtonElement>) {
    if (menu?.todo.id === todo.id) {
      setMenu(null);
      return;
    }
    const rect = e.currentTarget.getBoundingClientRect();
    setMenu({ todo, top: rect.bottom + 4, right: window.innerWidth - rect.right });
  }

  function editTodo(todo: Todo) {
    setMenu(null);
    setDialogState(todo);
  }

  function deleteTodo(todo: Todo) {
    setMenu(null);
    if (!window.confirm(`Delete "${todo.name}"?`)) {
      return;
    }
    todoService.delete(todo.id)
      .then(() => {
        showToast(`"${todo.name}" deleted.`, 'success');
        loadTodos();
      })
      .catch(() => showToast(`Failed to delete "${todo.name}".`, 'error'));
  }

  const filteredTodos = todos.filter(t =>
    t.name.toLowerCase().includes(searchTerm.toLowerCase())
  );

  return (
    <div className="todo-list">

      <div className="toolbar">
        <input
          value={searchTerm}
          onChange={e => setSearchTerm(e.target.value)}
          placeholder="Search"
        />
        <button className="btn-add" onClick={() => setDialogState('new')} aria-label="Add todo">
          +
        </button>
      </div>

      <div className="todo-list__card">
        {isLoading ? (
          <div className="loading-state">Loading todos…</div>
        ) : filteredTodos.length === 0 ? (
          <div className="empty-state">
            {searchTerm ? 'No todos match your search.' : 'No todos yet — add your first one.'}
          </div>
        ) : (
          <table className="todo-table">
            <thead>
              <tr>
                <th></th>
                <th>Name</th>
                <th>Description</th>
                <th>Created Time</th>
                <th>Is Completed</th>
                <th>Completed</th>
              </tr>
            </thead>
            <tbody>
              {filteredTodos.map(todo => (
                <tr key={todo.id} >
                  <td className="actions-cell">
                    <button
                      type="button"
                      className="actions-trigger"
                      aria-label="Open actions menu"
                      onClick={e => openMenu(todo, e)}
                    >
                      <EllipsisVertical size={18} />
                    </button>
                  </td>
                  <td className="todo-name">{todo.name}</td>
                  <td className="todo-description">{todo.description}</td>
                  <td>{new Date(todo.createdAt).toLocaleString()}</td>
                  <td>
                    {updatingIds.has(todo.id) ? (
                      <span className="status-checkbox__spinner" aria-label="Updating…" />
                    ) : (
                      <button
                        type="button"
                        className={`status-toggle${todo.isCompleted ? ' is-complete' : ''}`}
                        onClick={() => !todo.isCompleted && markComplete(todo)}
                      >
                        {todo.isCompleted ? <CircleCheckBig size={20} /> : <Circle size={20} />}
                      </button>
                    )}
                  </td>
                  <td>{(todo.completedAt !== null && todo.completedAt !== undefined) ? new Date(todo.completedAt).toLocaleString() : '—'}</td>
                </tr>
              ))}
            </tbody>
          </table>
        )}
      </div>

      {menu && createPortal(
        <>
          <div className="actions-menu__backdrop" onClick={() => setMenu(null)} />
          <div className="actions-menu" style={{ top: menu.top, right: menu.right }}>
            <button type="button" onClick={() => editTodo(menu.todo)}>Edit</button>
            <button type="button" className="actions-menu__delete" onClick={() => deleteTodo(menu.todo)}>Delete</button>
          </div>
        </>,
        document.body
      )}

      {dialogState && (
        <TodoDialog
          todo={dialogState === 'new' ? null : dialogState}
          onClose={() => setDialogState(null)}
          onSaved={loadTodos}
        />
      )}
    </div>
  );
}
