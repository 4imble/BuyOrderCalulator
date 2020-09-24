import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter, Route } from 'react-router-dom'
import './index.css';
import Order from './pages/order/Order';
import ViewOrder from './pages/order/ViewOrder';
import * as serviceWorker from './serviceWorker';

ReactDOM.render(
  //<React.StrictMode>
  <BrowserRouter>
      <Route exact path="/" component={Order} />
      <Route path="/order/:id" component={ViewOrder} />
  </BrowserRouter>,
  //</React.StrictMode>,
  document.getElementById('root')
);

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister();
