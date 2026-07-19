import { useEffect, useState } from 'react';
import { createPortal } from 'react-dom';
import { type Todo } from '../models/todo';
import { todoService } from '../services/todoService';
import './TodoDialog.css';

interface TodoDialogProps {
  todo?: Todo | null;
  onClose: () => void;
  onSaved: () => void;
}

export function TodoDialog({ todo, onClose, onSaved }: TodoDialogProps) {
  const [name, setName] = useState(todo?.name ?? '');
  const [description, setDescription] = useState(todo?.description ?? '');
  const [isSaving, setIsSaving] = useState(false);

  useEffect(() => {
    function onKeyDown(e: KeyboardEvent) {
      if (e.key === 'Escape') onClose();
    }
    document.addEventListener('keydown', onKeyDown);
    return () => document.removeEventListener('keydown', onKeyDown);
  }, [onClose]);

  function save() {
    setIsSaving(true);
    const request = { name, description };
    const result = todo
      ? todoService.update(todo.id, request)
      : todoService.create(request);

    result
      .then(() => {
        onSaved();
        onClose();
      })
      .finally(() => setIsSaving(false));
  }

  return createPortal(
    <div className="todo-dialog__backdrop" onClick={onClose}>
      <div className="todo-dialog" onClick={e => e.stopPropagation()}>
        <h2>{todo ? 'Edit Todo' : 'New Todo'}</h2>

        <label>
          Name
          <input
            value={name}
            onChange={e => setName(e.target.value)}
            placeholder="e.g. Buy groceries"
            autoFocus
          />
        </label>
        <label>
          Description
          <textarea
            rows={4}
            value={description}
            onChange={e => setDescription(e.target.value)}
            placeholder="Optional details"
          />
        </label>

        <div className="todo-dialog__actions">
          <button className="btn-secondary" onClick={onClose}>
            Cancel
          </button>
          <button className="btn-primary" onClick={save} disabled={!name.trim() || isSaving}>
            {isSaving ? 'Saving…' : 'Save'}
          </button>
        </div>
      </div>
    </div>,
    document.body
  );
}
