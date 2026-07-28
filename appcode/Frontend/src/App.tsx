import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import { lazy } from 'react';
import { TopHeader } from './shared/components/Header';
import { ToastContainer } from './shared/components/Toast';
import './App.css';

const TodoList = lazy(() =>
  import('./features/todo/components/TodoList').then(m => ({ default: m.TodoList }))
);


export function App() {
  return (
    <BrowserRouter>
      <div className="app-shell">
        <TopHeader />
        <main className="app-content">
            <Routes>
              <Route path="/" element={<Navigate to="/todo" replace />} />
              <Route path="/todo" element={<TodoList />} />
            </Routes>
        </main>
      </div>
      <ToastContainer />
    </BrowserRouter>
  );
}
