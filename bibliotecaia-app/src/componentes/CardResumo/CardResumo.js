import React from 'react';
import styles from './CardResumo.module.css';

function CardResumo({ titulo, valor, icone }) {
  return (
    <div className={styles.card}>
      <div className={styles.icone}>
        {icone}
      </div>

      <div>
        <span className={styles.titulo}>{titulo}</span>
        <h3 className={styles.valor}>{valor}</h3>
      </div>
    </div>
  );
}

export default CardResumo;