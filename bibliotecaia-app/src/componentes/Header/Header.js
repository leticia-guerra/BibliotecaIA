import React from 'react';
import { useNavigate } from 'react-router-dom';
import { Button } from 'react-bootstrap';
import { Container } from 'react-bootstrap';
import { FiLogOut } from 'react-icons/fi';
import { FaBookOpen } from 'react-icons/fa';
import styles from './Header.module.css';

function Header() {
    const navigate = useNavigate();
    const sair = () => {
    localStorage.removeItem('usuarioLogado');
    navigate('/');
    };
    return (
        <header className={styles.header}>
        <Container className="d-flex align-items-center">


            <div className={styles.logo}>
            <FaBookOpen size={22} />
            </div>


            <div>
                <h3 className={styles.titulo}>
                    Biblioteca Inteligente 
                </h3>
                <span className={styles.subtitulo}>
                    Organize suas leituras com IA
                </span>
            </div>

            <Button onClick={sair} className={`ms-auto ${styles.logoutButton}`} variant="link">
                <FiLogOut size={18} />
            </Button>
        </Container>
     </header>      
  );
}
export default Header;