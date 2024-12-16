import React from 'react'
import ReactDOM from 'react-dom/client'
import './index.css'
import App from './App.tsx'
import {searchCompanies} from './api.tsx';

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);

console.log(searchCompanies("aapl"));

root.render(
  <React.StrictMode>
    <App />
  </React.StrictMode>,
)
