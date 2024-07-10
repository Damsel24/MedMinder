import React from 'react';
import { RouterProvider } from 'react-router-dom';
import router from './components/app-router'; // Ensure the path is correct

const App = () => {
  return (
    <RouterProvider router={router} />
  );
};

export default App;
