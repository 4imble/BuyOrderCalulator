import React from 'react';
import { Button } from 'antd';
import './App.less';
import styles from './site.module.less';

export default class App extends React.Component {
  render() {
    return (
      <main className={styles.app}>
        <Button type="primary">Hello, Ant Design!</Button>
        <a href="foo.bar">I'm a link. Click me, please!</a>
        <span className={styles.gimble}>Gimble</span>
      </main>
    )
  };
}