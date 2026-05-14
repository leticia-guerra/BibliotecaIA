import React, { useEffect, useState } from 'react';
import { Container, Row, Col, Button, Form, Card } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import {
  FaBookOpen,
  FaFileAlt,
  FaStar,
  FaTags,
  FaCalendarAlt
} from 'react-icons/fa';

import styles from './Home.module.css';
import Header from '../../componentes/Header/Header';
import CardResumo from '../../componentes/CardResumo/CardResumo';
import { generos } from '../../utils/generos';

function Home() {
  const navigate = useNavigate();

  const usuarioLogado = JSON.parse(localStorage.getItem('usuarioLogado'));
  const usuarioEhAdmin = usuarioLogado?.tipoUsuario === 2;

  const [livros, setLivros] = useState([]);
  const [quantidadeVisivel, setQuantidadeVisivel] = useState(9);

  const [textoBusca, setTextoBusca] = useState('');
  const [generoFiltro, setGeneroFiltro] = useState('');

  const [generoSelecionado, setGeneroSelecionado] = useState('');
  const [respostaIA, setRespostaIA] = useState('');
  const [carregandoIA, setCarregandoIA] = useState(false);
  const [erroIA, setErroIA] = useState('');

  useEffect(() => {
    const buscarLivros = async () => {
      try {
        const response = await fetch(
          `http://localhost:5211/api/Livro/ListarPorUsuario/${usuarioLogado.id}`
        );

        const data = await response.json();
        setLivros(data);
      } catch (error) {
        console.error(error);
      }
    };

    buscarLivros();
  }, [usuarioLogado.id]);

  const gerarRecomendacoes = async () => {
    setErroIA('');
    setRespostaIA('');

    if (!generoSelecionado) {
      setErroIA('Selecione um gênero.');
      return;
    }

    try {
      setCarregandoIA(true);

      const response = await fetch(
        `http://localhost:5211/api/AI/recomendar-por-usuario/${usuarioLogado.id}?genero=${encodeURIComponent(generoSelecionado)}`
      );

      const data = await response.text();
      setRespostaIA(data);
    } catch (error) {
      setErroIA(error.message);
    } finally {
      setCarregandoIA(false);
    }
  };

  const recomendacoesFormatadas = respostaIA
    ? respostaIA.split(/(?=TÍTULO:)/).filter((item) => item.trim() !== '')
    : [];

  const extrairParte = (texto, marcador) => {
    const linha = texto
      .split('\n')
      .find((item) => item.toUpperCase().includes(marcador));

    return linha ? linha.replace(marcador, '').trim() : '';
  };

  const livrosFiltrados = livros.filter((livro) => {
    const texto = textoBusca.toLowerCase();

    const correspondeTexto =
      livro.titulo.toLowerCase().includes(texto) ||
      livro.autor.toLowerCase().includes(texto);

    const correspondeGenero =
      generoFiltro === '' || livro.genero === generoFiltro;

    return correspondeTexto && correspondeGenero;
  });

  const totalPaginas = livros.reduce(
    (total, livro) => total + livro.quantPaginas,
    0
  );

  const mediaAvaliacao =
    livros.length > 0
      ? (
          livros.reduce((total, livro) => total + livro.avaliacao, 0) /
          livros.length
        ).toFixed(1)
      : '0';

  const calcularGeneroFavorito = () => {
    if (livros.length === 0) {
      return 'Nenhum';
    }

    const contagem = {};

    livros.forEach((livro) => {
      contagem[livro.genero] = (contagem[livro.genero] || 0) + 1;
    });

    const generoMaisLido = Object.keys(contagem).reduce(
      (generoAtual, proximoGenero) => {
        return contagem[proximoGenero] > contagem[generoAtual]
          ? proximoGenero
          : generoAtual;
      }
    );

    return generoMaisLido;
  };

  const generoFavorito = calcularGeneroFavorito();
  const livrosVisiveis = livrosFiltrados.slice(0, quantidadeVisivel);

  const formatarData = (data) => {
    if (!data) return '';
    return new Date(data).toLocaleDateString('pt-BR');
  };

  return (
    <>
      <Header />

      <Container className="mt-4">
        <Row className="g-3">
          <Col md={3}>
            <CardResumo
              titulo="Livros lidos"
              valor={livros.length}
              icone={<FaBookOpen />}
            />
          </Col>

          <Col md={3}>
            <CardResumo
              titulo="Páginas lidas"
              valor={totalPaginas}
              icone={<FaFileAlt />}
            />
          </Col>

          <Col md={3}>
            <CardResumo
              titulo="Avaliação média"
              valor={mediaAvaliacao}
              icone={<FaStar />}
            />
          </Col>

          <Col md={3}>
            <CardResumo
              titulo="Gênero favorito"
              valor={generoFavorito}
              icone={<FaTags />}
            />
          </Col>
        </Row>

        {usuarioEhAdmin && (
          <div className="mt-4 text-end">
            <Button
              className={styles.botaoPrincipal}
              onClick={() => navigate('/admin')}
            >
              Painel Admin
            </Button>
          </div>
        )}

        <h4 className="mt-5 mb-3">Recomendações IA</h4>

        <Card className="p-4 mb-5">
          <Row className="g-3 align-items-end">
            <Col md={8}>
              <Form.Select
                value={generoSelecionado}
                onChange={(e) => setGeneroSelecionado(e.target.value)}
              >
                <option value="">Selecione um gênero</option>

                {generos.map((g) => (
                  <option key={g} value={g}>
                    {g}
                  </option>
                ))}
              </Form.Select>
            </Col>

            <Col md={4}>
              <Button
                onClick={gerarRecomendacoes}
                className={styles.botaoPrincipal}
              >
                {carregandoIA ? 'Gerando...' : 'Gerar recomendações'}
              </Button>
            </Col>
          </Row>

          {erroIA && <p className="text-danger mt-3">{erroIA}</p>}

          {recomendacoesFormatadas.length > 0 && (
            <Row className="g-3 mt-4">
              {recomendacoesFormatadas.map((rec, index) => {
                const titulo = extrairParte(rec, 'TÍTULO:');
                const autor = extrairParte(rec, 'AUTOR:');
                const paginasTexto = extrairParte(rec, 'PÁGINAS:');
                const paginas = paginasTexto.replace(/\D/g, '');

                const justificativa = rec
                  .replace(/TÍTULO:.*\n?/i, '')
                  .replace(/AUTOR:.*\n?/i, '')
                  .replace(/PÁGINAS:.*\n?/i, '')
                  .replace(/JUSTIFICATIVA:/i, '')
                  .replace(/---/g, '')
                  .trim();

                return (
                  <Col md={4} key={index}>
                    <div className={styles.cardRecomendacaoIA}>
                      <h5 className={styles.tituloIA}>{titulo}</h5>

                      <p className={styles.autorIA}>{autor}</p>

                      {paginas && (
                        <p className={styles.infoLivro}>
                          <FaBookOpen className="me-2" />
                          {paginas} páginas
                        </p>
                      )}

                      <p className={styles.justificativaIA}>{justificativa}</p>

                      <Button
                        className={`${styles.botaoPrincipal} mt-3`}
                        onClick={() =>
                          navigate('/livros/cadastrar', {
                            state: {
                              titulo,
                              autor,
                              genero: generoSelecionado,
                              quantPaginas: paginas
                            }
                          })
                        }
                      >
                        Adicionar livro
                      </Button>
                    </div>
                  </Col>
                );
              })}
            </Row>
          )}
        </Card>

        <div className="d-flex justify-content-between align-items-center mt-5 mb-3">
          <h4>Meus Livros ({livros.length})</h4>

          <Button
            onClick={() => navigate('/livros/cadastrar')}
            className={styles.botaoPrincipal}
          >
            + Adicionar Livro
          </Button>
        </div>

        <Row className="mb-4 g-3">
          <Col xs={12} md={7}>
            <Form.Control
              placeholder="Buscar por título ou autor..."
              value={textoBusca}
              onChange={(e) => {
                setTextoBusca(e.target.value);
                setQuantidadeVisivel(9);
              }}
            />
          </Col>

          <Col xs={12} md={5}>
            <Form.Select
              value={generoFiltro}
              onChange={(e) => {
                setGeneroFiltro(e.target.value);
                setQuantidadeVisivel(9);
              }}
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
          {livrosVisiveis.map((livro) => (
            <Col md={4} key={livro.id}>
              <div className={styles.cardLivro}>
                <h5 className={styles.tituloLivro}>{livro.titulo}</h5>

                <p className={styles.autorLivro}>{livro.autor}</p>

                <p className={styles.infoLivro}>
                  <FaStar className="me-2" />
                  {livro.avaliacao}
                </p>

                <p className={styles.infoLivro}>
                  <FaBookOpen className="me-2" />
                  {livro.quantPaginas} páginas
                </p>

                <p className={styles.infoLivro}>
                  <FaCalendarAlt className="me-2" />
                  {formatarData(livro.dataLeitura)}
                </p>

                <span className={styles.generoLivro}>
                  {livro.genero}
                </span>

                <p className={styles.comentarioLivro}>
                  "{livro.comentario}"
                </p>
              </div>
            </Col>
          ))}
        </Row>

        {quantidadeVisivel < livrosFiltrados.length && (
          <div className="text-center mt-4 mb-5">
            <Button
              className={styles.botaoPrincipal}
              onClick={() => setQuantidadeVisivel(quantidadeVisivel + 9)}
            >
              Ver mais
            </Button>
          </div>
        )}
        <div className={styles.rodapeHome}>
          BibliotecaIA — Organize suas leituras e descubra novas recomendações com inteligência artificial.
        </div>
      </Container>
    </>
  );
}

export default Home;