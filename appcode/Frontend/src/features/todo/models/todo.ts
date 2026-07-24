export interface Todo {
  id: number;
  name: string;
  description?: string;
  createdAt: string;
  isCompleted: boolean;
  completedAt?: string | null;
}

export interface CreateTodoRequest {
  name: string;
  description?: string;
}

export interface UpdateTodoRequest {
  name?: string;
  description?: string;
}
