import React, { useEffect, useState } from 'react';
import { Container, Row, Col, Form, Button } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import { FaArrowLeft, FaUser, FaEnvelope, FaUserShield } from 'react-icons/fa';

import Header from '../../componentes/Header/Header';
import styles from './UsuariosAdmin.module.css';

function UsuariosAdmin() {
  const navigate = useNavigate();

  const [usuarios, setUsuarios] = useState([]);
  const [textoBusca, setTextoBusca] = useState('');

  useEffect(() => {
    const buscarUsuarios = async () => {
      try {
        const response = await fetch('http://localhost:5211/api/Usuario/Listar/true');
        const data = await response.json();

        setUsuarios(data);
      } catch (error) {
        console.error(error);
      }
    };

    buscarUsuarios();
  }, []);

  const usuariosFiltrados = usuarios.filter((usuario) => {
    const texto = textoBusca.toLowerCase();

    return (
      usuario.nome.toLowerCase().includes(texto) ||
      usuario.email.toLowerCase().includes(texto) ||
      usuario.tipoUsuario.toLowerCase().includes(texto)
    );
  });

  return (
    <>
      <Header />

      <Container className="mt-4 mb-5">
        <div className="d-flex justify-content-between align-items-center mb-4">
          <h3 className="mb-0">Usuários Cadastrados</h3>

          <Button
            className={styles.botaoVoltar}
            onClick={() => navigate('/admin')}
          >
            <FaArrowLeft className="me-2" />
            Voltar
          </Button>
        </div>

        <Form.Control
          className="mb-4"
          placeholder="Buscar por nome, email ou tipo..."
          value={textoBusca}
          onChange={(e) => setTextoBusca(e.target.value)}
        />

        <Row className="g-4">
          {usuariosFiltrados.map((usuario) => (
            <Col xs={12} md={6} lg={4} key={usuario.id}>
              <div className={styles.cardUsuario}>
                <h5 className={styles.nome}>
                  <FaUser className="me-2" />
                  {usuario.nome}
                </h5>

                <p className={styles.info}>
                  <FaEnvelope className="me-2" />
                  {usuario.email}
                </p>

                <span className={styles.tipo}>
                  <FaUserShield className="me-2" />
                  {usuario.tipoUsuario}
                </span>
              </div>
            </Col>
          ))}
        </Row>
      </Container>
    </>
  );
}

export default UsuariosAdmin;