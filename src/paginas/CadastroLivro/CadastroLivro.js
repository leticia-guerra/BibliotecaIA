import React, { useState, useEffect } from 'react';
import { Container, Form, Button, Row, Col, Card } from 'react-bootstrap';
import { useNavigate, useLocation } from 'react-router-dom';
import { FaArrowLeft } from 'react-icons/fa';

import Header from '../../componentes/Header/Header';
import { generos } from '../../utils/generos';
import styles from './CadastroLivro.module.css';

function CadastroLivro() {
  const navigate = useNavigate();
  const location = useLocation();

  const livroRecebido = location.state;

  const [tituloBusca, setTituloBusca] = useState('');
  const [resultados, setResultados] = useState([]);

  const [titulo, setTitulo] = useState(livroRecebido?.titulo || '');
  const [autor, setAutor] = useState(livroRecebido?.autor || '');
  const [genero, setGenero] = useState(livroRecebido?.genero || '');
  const [quantPaginas, setQuantPaginas] = useState(livroRecebido?.quantPaginas || '');
  const [dataLeitura, setDataLeitura] = useState('');
  const [avaliacao, setAvaliacao] = useState('');
  const [comentario, setComentario] = useState('');
  const [erro, setErro] = useState('');

  useEffect(() => {
    const buscarLivroCatalogo = async () => {
      if (!livroRecebido?.titulo) {
        return;
      }

      try {
        const response = await fetch(
          `http://localhost:5211/api/CatalogoLivro/BuscarPorTitulo?titulo=${livroRecebido.titulo}`
        );

        if (!response.ok) {
          return;
        }

        const data = await response.json();

        if (data.length > 0) {
          const livroCatalogo = data[0];

          setGenero(livroCatalogo.genero || livroRecebido?.genero || '');
          setQuantPaginas(livroCatalogo.quantPaginas || '');
        }
      } catch (error) {
        console.error(error);
      }
    };

    buscarLivroCatalogo();
  }, [livroRecebido]);

  const buscarLivro = async () => {
    setErro('');
    setResultados([]);

    try {
      const responseCatalogo = await fetch(
        `http://localhost:5211/api/CatalogoLivro/BuscarPorTitulo?titulo=${tituloBusca}`
      );

      if (!responseCatalogo.ok) {
        throw new Error('Erro ao buscar livros do catálogo');
      }

      const dataCatalogo = await responseCatalogo.json();

      let dataLivros = [];

      try {
        const responseLivros = await fetch(
          'http://localhost:5211/api/Livro/Listar/true'
        );

        if (responseLivros.ok) {
          dataLivros = await responseLivros.json();
        }
      } catch (error) {
        console.error(error);
      }

      const livrosUsuarios = dataLivros.filter((livro) =>
        livro.titulo
          ?.toLowerCase()
          .includes(tituloBusca.toLowerCase())
      );

      const todosLivros = [
        ...dataCatalogo.map((livro) => ({
          ...livro,
          origem: 'Catálogo'
        })),

        ...livrosUsuarios.map((livro) => ({
          ...livro,
          origem: 'Usuário'
        }))
      ];

      const resultadosSemDuplicados = todosLivros.filter((livro, index, self) =>
        index === self.findIndex((item) =>
          item.titulo?.toLowerCase() === livro.titulo?.toLowerCase() &&
          item.autor?.toLowerCase() === livro.autor?.toLowerCase()
        )
      );

      setResultados(resultadosSemDuplicados);
    } catch (err) {
      setErro(err.message);
    }
  };

  const usarLivro = (livro) => {
    setTitulo(livro.titulo);
    setAutor(livro.autor);
    setGenero(livro.genero);
    setQuantPaginas(livro.quantPaginas);
  };

  const cadastrarLivro = async (e) => {
    e.preventDefault();

    const usuarioLogado = JSON.parse(localStorage.getItem('usuarioLogado'));

    try {
      const response = await fetch('http://localhost:5211/api/Livro/Criar', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify({
          titulo,
          autor,
          genero,
          quantPaginas: Number(quantPaginas),
          dataLeitura,
          avaliacao: Number(avaliacao),
          comentario,
          usuarioID: usuarioLogado.id
        })
      });

      if (!response.ok) {
        const mensagem = await response.text();
        throw new Error(mensagem);
      }

      alert('Livro cadastrado com sucesso!');
      navigate('/home');
    } catch (err) {
      setErro(err.message);
    }
  };

  return (
    <>
      <Header />

      <Container className={styles.container}>
        <div className="d-flex justify-content-between align-items-center mb-4">
          <h3 className="mb-0">Cadastrar livro lido</h3>

          <Button
            className={styles.botaoVoltar}
            onClick={() => navigate('/home')}
          >
            <FaArrowLeft className="me-2" />
            Voltar
          </Button>
        </div>

        {erro && <p className="text-danger">{erro}</p>}

        <Card className="mb-4 p-3">
          <Form.Label>Pesquisar livro antes de cadastrar</Form.Label>

          <Row className="g-3">
            <Col md={9}>
              <Form.Control
                placeholder="Digite o título do livro"
                value={tituloBusca}
                onChange={(e) => setTituloBusca(e.target.value)}
              />
            </Col>

            <Col md={3}>
              <Button onClick={buscarLivro} className={styles.botao}>
                Buscar
              </Button>
            </Col>
          </Row>

          {resultados.length > 0 && (
            <div className="mt-3">
              <strong>Livros encontrados:</strong>

              {resultados.map((livro) => (
                <div
                  key={`${livro.origem}-${livro.id}`}
                  className={styles.resultado}
                >
                  <div>
                    <strong>{livro.titulo}</strong>
                    <br />
                    <span>{livro.autor}</span>
                  </div>

                  <Button
                    size="sm"
                    onClick={() => usarLivro(livro)}
                    className={styles.botaoPequeno}
                  >
                    Usar este livro
                  </Button>
                </div>
              ))}
            </div>
          )}
        </Card>

        <Form onSubmit={cadastrarLivro} className={styles.form}>
          <Form.Group className="mb-3">
            <Form.Label>Título</Form.Label>
            <Form.Control
              value={titulo}
              onChange={(e) => setTitulo(e.target.value)}
            />
          </Form.Group>

          <Form.Group className="mb-3">
            <Form.Label>Autor</Form.Label>
            <Form.Control
              value={autor}
              onChange={(e) => setAutor(e.target.value)}
            />
          </Form.Group>

          <Form.Group className="mb-3">
            <Form.Label>Gênero</Form.Label>
            <Form.Select
              value={genero}
              onChange={(e) => setGenero(e.target.value)}
            >
              <option value="">Selecione um gênero</option>

              {generos.map((item) => (
                <option key={item} value={item}>
                  {item}
                </option>
              ))}
            </Form.Select>
          </Form.Group>

          <Form.Group className="mb-3">
            <Form.Label>Quantidade de páginas</Form.Label>
            <Form.Control
              type="number"
              value={quantPaginas}
              onChange={(e) => setQuantPaginas(e.target.value)}
            />
          </Form.Group>

          <Form.Group className="mb-3">
            <Form.Label>Data de leitura</Form.Label>
            <Form.Control
              type="date"
              value={dataLeitura}
              onChange={(e) => setDataLeitura(e.target.value)}
            />
          </Form.Group>

          <Form.Group className="mb-3">
            <Form.Label>Avaliação</Form.Label>
            <Form.Control
              type="number"
              min="1"
              max="5"
              value={avaliacao}
              onChange={(e) => setAvaliacao(e.target.value)}
            />
          </Form.Group>

          <Form.Group className="mb-3">
            <Form.Label>Comentário</Form.Label>
            <Form.Control
              as="textarea"
              rows={3}
              value={comentario}
              onChange={(e) => setComentario(e.target.value)}
            />
          </Form.Group>

          <Button type="submit" className={styles.botaoSalvar}>
            Salvar livro
          </Button>
        </Form>
      </Container>
    </>
  );
}

export default CadastroLivro;