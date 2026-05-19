import React from 'react';
import { BrowserRouter, Routes, Route } from 'react-router-dom';

import Home from './paginas/Home/Home';
import Login from './paginas/Login/Login';
import Cadastro from './paginas/Cadastro/Cadastro';
import CadastroLivro from './paginas/CadastroLivro/CadastroLivro';
import Admin from './paginas/Admin/Admin';
import Catalogo from './paginas/Catalogo/Catalogo';
import UsuariosAdmin from './paginas/UsuarioAdmin/UsuariosAdmin';

import PrivateRoute from './componentes/PrivateRoute/PrivateRoute';
import PrivateRouteAdmin from './routes/PrivateRouteAdmin';

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Login />} />

        <Route path="/cadastro" element={<Cadastro />} />

        <Route
          path="/home"
          element={
            <PrivateRoute>
              <Home />
            </PrivateRoute>
          }
        />

        <Route
          path="/livros/cadastrar"
          element={
            <PrivateRoute>
              <CadastroLivro />
            </PrivateRoute>
          }
        />

        <Route
          path="/admin"
          element={
            <PrivateRouteAdmin>
              <Admin />
            </PrivateRouteAdmin>
          }
        />

        <Route
          path="/admin/catalogo"
          element={
            <PrivateRouteAdmin>
              <Catalogo />
            </PrivateRouteAdmin>
          }
        />

        <Route
          path="/admin/usuarios"
          element={
            <PrivateRouteAdmin>
              <UsuariosAdmin />
            </PrivateRouteAdmin>
          }
        />
      </Routes>
    </BrowserRouter>
  );
}

export default App;