import React from 'react';
import { Navigate } from 'react-router-dom';

function PrivateRoute({ children }) {
  const usuarioLogado = localStorage.getItem('usuarioLogado');

  if (!usuarioLogado) {
    return <Navigate to="/" />;
  }

  return children;
}

export default PrivateRoute;