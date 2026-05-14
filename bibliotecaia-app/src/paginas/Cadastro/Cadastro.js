import React, { useState } from 'react';
import { Container, Form, Button } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import styles from './Cadastro.module.css';

function Cadastro() {
  const [nome, setNome] = useState('');
  const [email, setEmail] = useState('');
  const [senha, setSenha] = useState('');
  const [erro, setErro] = useState('');

  const navigate = useNavigate();

  const handleCadastro = async (e) => {
    e.preventDefault();

    try {
      const response = await fetch('http://localhost:5211/api/Usuario/Criar', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify({
          nome,
          email,
          senha,
          tipoUsuario: 1
        })
      });

      if (!response.ok) {
        const mensagem = await response.text();
        throw new Error(mensagem);
      }

      alert('Usuário cadastrado com sucesso!');
      navigate('/');
    } catch (err) {
      setErro(err.message);
    }
  };

  return (
    <Container className={styles.container}>
      <Form onSubmit={handleCadastro} className={styles.form}>
        <h3 className="mb-4">Criar conta</h3>

        {erro && <p className="text-danger">{erro}</p>}

        <Form.Group className="mb-3">
          <Form.Label>Nome</Form.Label>
          <Form.Control value={nome} onChange={(e) => setNome(e.target.value)} />
        </Form.Group>

        <Form.Group className="mb-3">
          <Form.Label>Email</Form.Label>
          <Form.Control type="email" value={email} onChange={(e) => setEmail(e.target.value)} />
        </Form.Group>

        <Form.Group className="mb-3">
          <Form.Label>Senha</Form.Label>
          <Form.Control type="password" value={senha} onChange={(e) => setSenha(e.target.value)} />
        </Form.Group>

        <Button type="submit" className="w-100">
          Cadastrar
        </Button>

        <div className="text-center mt-3">
          <span>Já tem conta? </span>
          <a href="/">Entrar</a>
        </div>
      </Form>
    </Container>
  );
}

export default Cadastro;