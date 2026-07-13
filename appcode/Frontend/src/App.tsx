import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import { lazy, Suspense } from 'react';
import { TopHeader } from './shared/components/Header';
import './App.css';

const TodoList = lazy(() =>
  import('./features/todo/components/TodoList').then(m => ({ default: m.TodoList }))
);
const TodoDetail = lazy(() =>
  import('./features/todo/components/TodoDetail').then(m => ({ default: m.TodoDetail }))
);

export function App() {
  return (
    <BrowserRouter>
      <div className="app-shell">
        <TopHeader />
        <main className="app-content">
          <Suspense fallback={<div className="loading-state">Loading…</div>}>
            <Routes>
              <Route path="/" element={<Navigate to="/todo" replace />} />
              <Route path="/todo" element={<TodoList />} />
              <Route path="/todo/new" element={<TodoDetail />} />
              <Route path="/todo/:id" element={<TodoDetail />} />
            </Routes>
          </Suspense>
        </main>
      </div>
    </BrowserRouter>
  );
}
