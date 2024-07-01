import React from 'react';
import { Routes, Route } from 'react-router-dom';
import Home from './components/Home';
import Product from './components/Product';
import EditProduct from './components/EditProduct';
import AppSidebar from './components/AppSidebar';
import AppHeader from './components/AppHeader';
import AppFooter from './components/AppFooter';
import { ThemeProvider, createTheme } from '@mui/material/styles';
const theme = createTheme();

const App = () => {
  return (
    <ThemeProvider theme={theme}>
    <React.Fragment>
      <AppSidebar />
      <div className="wrapper d-flex flex-column min-vh-100">
        <AppHeader />
        <div className="body flex-grow-1" style={{ marginLeft: 260, marginRight: 1 }}>
          <Routes>
            <Route path="/" element={<Home />} />
            <Route path="/add/product" element={<Product />} />
            <Route path="/edit/product/:id" element={<EditProduct />} />
          </Routes>
        </div>
        <AppFooter />
      </div>
    </React.Fragment>
    </ThemeProvider>
  );
};

export default App;
