import { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { todoService } from '../services/todoService';
import './TodoDetail.css';

export function TodoDetail() {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const [name, setName] = useState('');
  const [description, setDescription] = useState('');
  const [isSaving, setIsSaving] = useState(false);

  useEffect(() => {
    if (id) {
      todoService.getById(+id).then(todo => {
        setName(todo.name);
        setDescription(todo.description ?? '');
      });
    }
  }, [id]);

  function save() {
    setIsSaving(true);
    const request = { name, description };
    const result = id
      ? todoService.update(+id, request)
      : todoService.create(request);

    result
      .then(() => navigate('/todo'))
      .finally(() => setIsSaving(false));
  }

  return (
    <div className="todo-detail">
      <h2>{id ? 'Edit Todo' : 'New Todo'}</h2>

      <label>
        Name
        <input
          value={name}
          onChange={e => setName(e.target.value)}
          placeholder="e.g. Buy groceries"
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

      <div className="todo-detail__actions">
        <button className="btn-primary" onClick={save} disabled={!name.trim() || isSaving}>
          {isSaving ? 'Saving…' : 'Save'}
        </button>
        <button className="btn-secondary" onClick={() => navigate('/todo')}>
          Cancel
        </button>
      </div>
    </div>
  );
}
