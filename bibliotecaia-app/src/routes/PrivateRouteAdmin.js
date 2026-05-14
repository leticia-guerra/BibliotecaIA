import { Navigate } from 'react-router-dom';

function PrivateRouteAdmin({ children }) {
  const usuarioLogado = JSON.parse(localStorage.getItem('usuarioLogado'));

  if (!usuarioLogado) {
    return <Navigate to="/" />;
  }

  if (usuarioLogado.tipoUsuario !== 2) {
    return <Navigate to="/home" />;
  }

  return children;
}

export default PrivateRouteAdmin;