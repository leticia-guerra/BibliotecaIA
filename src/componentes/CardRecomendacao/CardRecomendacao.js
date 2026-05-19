import React from 'react';
import styles from './CardRecomendacao.module.css';
import { FaBook } from 'react-icons/fa';

function CardRecomendacao({ titulo, autor, descricao }) {
  return (
    <div className={styles.card}>
      <div className={styles.icone}>
        <FaBook />
      </div>

      <div>
        <h5 className={styles.titulo}>{titulo}</h5>
        <span className={styles.autor}>{autor}</span>
        <p className={styles.descricao}>{descricao}</p>
      </div>
    </div>
  );
}

export default CardRecomendacao;