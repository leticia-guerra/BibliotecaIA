import React from 'react';
import { Container, Row, Col, Card, Button } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import { FaBookOpen, FaUsers, FaArrowRight } from 'react-icons/fa';

import Header from '../../componentes/Header/Header';
import styles from './Admin.module.css';

function Admin() {
  const navigate = useNavigate();

  return (
    <>
      <Header />

      <Container className="mt-4">
        <h3 className="mb-4">Painel Administrativo</h3>

        <Row className="g-4">
          <Col xs={12} md={6}>
            <Card className={styles.cardAdmin}>
              <div className={styles.icone}>
                <FaBookOpen />
              </div>

              <h5>Catálogo de Livros</h5>
              <p>Visualize os livros cadastrados no catálogo geral do sistema.</p>

              <Button
                className={styles.botao}
                onClick={() => navigate('/admin/catalogo')}
              >
                Gerenciar catálogo <FaArrowRight className="ms-2" />
              </Button>
            </Card>
          </Col>

          <Col xs={12} md={6}>
            <Card className={styles.cardAdmin}>
              <div className={styles.icone}>
                <FaUsers />
              </div>

              <h5>Usuários</h5>
              <p>Área administrativa para visualizar usuários do sistema.</p>

              <Button
              className={styles.botao}
              onClick={() => navigate('/admin/usuarios')}
            >
              Gerenciar usuários
              <FaArrowRight className="ms-2" />
            </Button>
            </Card>
          </Col>
        </Row>
      </Container>
    </>
  );
}

export default Admin;