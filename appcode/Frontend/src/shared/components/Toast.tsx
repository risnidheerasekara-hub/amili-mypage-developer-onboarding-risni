import { useEffect, useState } from 'react';
import { createPortal } from 'react-dom';
import './Toast.css';

export type ToastType = 'success' | 'error';

interface ToastMessage {
  id: number;
  message: string;
  type: ToastType;
}

type Listener = (toast: ToastMessage) => void;

let nextId = 0;
const listeners = new Set<Listener>();

export function showToast(message: string, type: ToastType = 'success') {
  const toast: ToastMessage = { id: nextId++, message, type };
  listeners.forEach(listener => listener(toast));
}

export function ToastContainer() {
  const [toasts, setToasts] = useState<ToastMessage[]>([]);

  useEffect(() => {
    const listener: Listener = toast => {
      setToasts(prev => [...prev, toast]);
      setTimeout(() => {
        setToasts(prev => prev.filter(t => t.id !== toast.id));
      }, 3000);
    };
    listeners.add(listener);
    return () => { listeners.delete(listener); };
  }, []);

  if (toasts.length === 0) return null;

  return createPortal(
    <div className="toast-stack">
      {toasts.map(toast => (
        <div key={toast.id} className={`toast toast--${toast.type}`}>
          {toast.message}
        </div>
      ))}
    </div>,
    document.body
  );
}
