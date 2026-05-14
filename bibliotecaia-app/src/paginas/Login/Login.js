import React, { useState } from 'react';
import { Container, Form, Button } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import styles from './Login.module.css';

function Login() {
  const [email, setEmail] = useState('');
  const [senha, setSenha] = useState('');
  const [erro, setErro] = useState('');

  const navigate = useNavigate();

  const handleLogin = async (e) => {
    e.preventDefault();

    try {
      const response = await fetch('http://localhost:5211/api/Usuario/Login', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify({ email, senha })
      });

      if (!response.ok) {
        const mensagem = await response.text();
        throw new Error(mensagem);
      }

      const data = await response.json();

      // salvar usuário
      localStorage.setItem('usuarioLogado', JSON.stringify(data));

      if (data.tipoUsuario === 2) {
        navigate('/admin');
      } else {
        navigate('/home');
      }

    } catch (err) {
      setErro(err.message);
    }
  };

  return (
    <Container className={styles.container}>
      <Form onSubmit={handleLogin} className={styles.form}>

        <h3 className="mb-4">Login</h3>

        {erro && <p className="text-danger">{erro}</p>}

        <Form.Group className="mb-3">
          <Form.Label>Email</Form.Label>
          <Form.Control
            type="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />
        </Form.Group>

        <Form.Group className="mb-3">
          <Form.Label>Senha</Form.Label>
          <Form.Control
            type="password"
            value={senha}
            onChange={(e) => setSenha(e.target.value)}
          />
        </Form.Group>

        <Button type="submit" className="w-100">
          Entrar
        </Button>
        <div className="text-center mt-3">
            <span>Não tem conta? </span>
            <a href="/cadastro">Criar conta</a>
        </div>

      </Form>
    </Container>
  );
}

export default Login;