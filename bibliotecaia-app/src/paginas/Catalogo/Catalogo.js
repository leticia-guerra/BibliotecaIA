import React, { useEffect, useState } from 'react';
import { Container, Row, Col, Form, Button } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import { FaArrowLeft, FaBookOpen } from 'react-icons/fa';

import Header from '../../componentes/Header/Header';
import { generos } from '../../utils/generos';
import styles from './Catalogo.module.css';

function Catalogo() {
  const navigate = useNavigate();

  const [livros, setLivros] = useState([]);
  const [textoBusca, setTextoBusca] = useState('');
  const [generoFiltro, setGeneroFiltro] = useState('');

  useEffect(() => {
  const buscarCatalogo = async () => {
    try {
      const response = await fetch('http://localhost:5211/api/LivroSql/listar-view');
      const data = await response.json();
      setLivros(data);
    } catch (error) {
      console.error(error);
    }
  };

  buscarCatalogo();
}, []);

  const livrosFiltrados = livros.filter((livro) => {
    const texto = textoBusca.toLowerCase();

    const correspondeTexto =
      livro.titulo.toLowerCase().includes(texto) ||
      livro.autor.toLowerCase().includes(texto);

    const correspondeGenero =
      generoFiltro === '' || livro.genero === generoFiltro;

    return correspondeTexto && correspondeGenero;
  });

  return (
    <>
      <Header />

      <Container className="mt-4 mb-5">
        <div className="d-flex justify-content-between align-items-center mb-4">
          <h3 className="mb-0">Catálogo de Livros</h3>

          <Button
            className={styles.botaoVoltar}
            onClick={() => navigate('/admin')}
          >
            <FaArrowLeft className="me-2" />
            Voltar
          </Button>
        </div>

        <Row className="mb-4 g-3">
          <Col xs={12} md={7}>
            <Form.Control
              placeholder="Buscar por título ou autor..."
              value={textoBusca}
              onChange={(e) => setTextoBusca(e.target.value)}
            />
          </Col>

          <Col xs={12} md={5}>
            <Form.Select
              value={generoFiltro}
              onChange={(e) => setGeneroFiltro(e.target.value)}
            >
              <option value="">Todos os gêneros</option>

              {generos.map((genero) => (
                <option key={genero} value={genero}>
                  {genero}
                </option>
              ))}
            </Form.Select>
          </Col>
        </Row>

        <Row className="g-4">
          {livrosFiltrados.map((livro) => (
            <Col xs={12} md={6} lg={4} key={livro.id}>
              <div className={styles.cardCatalogo}>
                <h5 className={styles.titulo}>{livro.titulo}</h5>
                <p className={styles.autor}>{livro.autor}</p>

                <p className={styles.info}>
                  <FaBookOpen className="me-2" />
                  {livro.quantPaginas} páginas
                </p>

                <span className={styles.genero}>
                  {livro.genero}
                </span>

                
              </div>
            </Col>
          ))}
        </Row>
      </Container>
    </>
  );
}

export default Catalogo;