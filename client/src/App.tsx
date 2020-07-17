import React from 'react';
import { Layout, Row, Col } from 'antd';
import './App.less';
import styles from './site.module.less';

const { Header, Footer, Content } = Layout;

export default class App extends React.Component {
  render() {
    return (
      <main>
        <Layout>
          <Header>Header</Header>
          <Content style={{ padding: '50px' }}>
            <Row>
              <Col>
                <svg xmlns="http://www.w3.org/2000/svg" version="1.1" width="100%" height="300">
                  <polygon className={styles.hex} points="300,150, 225,280 75,280 0,150 75,20 225,20"></polygon>
                </svg>
              </Col>
            </Row>
          </Content>
          <Footer>Footer</Footer>
        </Layout>
      </main >
    )
  };
}