import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { type Todo } from '../models/todo';
import { todoService } from '../services/todoService';
import './TodoList.css';

export function TodoList() {
  const [todos, setTodos] = useState<Todo[]>([]);
  const [searchTerm, setSearchTerm] = useState('');
  const [isLoading, setIsLoading] = useState(true);
  const navigate = useNavigate();

  useEffect(() => { loadTodos(); }, []);

  function loadTodos() {
    setIsLoading(true);
    todoService.getAll()
      .then(setTodos)
      .finally(() => setIsLoading(false));
  }

  function toggleComplete(todo: Todo) {
    todoService.updateComplete(todo.id, !todo.isCompleted).then(loadTodos);
  }

  const filteredTodos = todos.filter(t =>
    t.name.toLowerCase().includes(searchTerm.toLowerCase())
  );

  return (
    <div className="todo-list">
      <div className="todo-list__header">
        <h2>Todos</h2>
      </div>

      <div className="toolbar">
        <input
          value={searchTerm}
          onChange={e => setSearchTerm(e.target.value)}
          placeholder="Search todos…"
        />
        <button className="btn-add" onClick={() => navigate('/todo/new')} aria-label="Add todo">
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
                <th>Name</th><th>Description</th><th>Created Time</th>
                <th>Is Completed</th><th>Completed</th>
              </tr>
            </thead>
            <tbody>
              {filteredTodos.map(todo => (
                <tr key={todo.id} onClick={() => navigate(`/todo/${todo.id}`)}>
                  <td className="todo-name">{todo.name}</td>
                  <td className="todo-description">{todo.description}</td>
                  <td>{new Date(todo.createdAt).toLocaleString()}</td>
                  <td>
                    <button
                      type="button"
                      className={`status-toggle${todo.isCompleted ? ' is-complete' : ''}`}
                      onClick={e => { e.stopPropagation(); toggleComplete(todo); }}
                      aria-label={todo.isCompleted ? 'Mark as incomplete' : 'Mark as complete'}
                    >
                      {todo.isCompleted && (
                        <svg viewBox="0 0 16 16" fill="none">
                          <path d="M3 8.5 6.5 12 13 4" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round" />
                        </svg>
                      )}
                    </button>
                  </td>
                  <td>{todo.completedAt ? new Date(todo.completedAt).toLocaleString() : '—'}</td>
                </tr>
              ))}
            </tbody>
          </table>
        )}
      </div>
    </div>
  );
}
