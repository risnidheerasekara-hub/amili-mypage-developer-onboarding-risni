import { API_BASE_URL } from '../../../config';
import { type Todo, type CreateTodoRequest, type UpdateTodoRequest } from '../models/todo';
const baseUrl = `${API_BASE_URL}/api/TodoItems`;

async function handleResponse<T>(res: Response): Promise<T> {
  if (!res.ok) throw new Error(`Request failed: ${res.status}`);
  return res.status === 204 ? (undefined as T) : res.json();
}

export const todoService = {
  getAll: (): Promise<Todo[]> =>
    fetch(baseUrl).then(res => handleResponse<Todo[]>(res)),

  getById: (id: number): Promise<Todo> =>
    fetch(`${baseUrl}/${id}`).then(res => handleResponse<Todo>(res)),

  create: (request: CreateTodoRequest): Promise<Todo> =>
    fetch(baseUrl, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(request),
    }).then(res => handleResponse<Todo>(res)),

  update: (id: number, request: UpdateTodoRequest): Promise<Todo> =>
    fetch(`${baseUrl}/${id}`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(request),
    }).then(res => handleResponse<Todo>(res)),

  delete: (id: number): Promise<void> =>
    fetch(`${baseUrl}/${id}`, { method: 'DELETE' }).then(res => handleResponse<void>(res)),

  updateComplete: (id: number): Promise<Todo> =>
    fetch(`${baseUrl}/${id}/complete`, {
      method: 'PATCH',
      headers: { 'Content-Type': 'application/json' },
    }).then(res => handleResponse<Todo>(res)),
};
