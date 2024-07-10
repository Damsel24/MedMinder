import { createBrowserRouter } from 'react-router-dom';
import Home from '../pages/HomePage';
import Patient from '../pages/PatientPage';
import AboutUs from '../pages/AboutUsPage';
import Contact from '../pages/ContactUsPage';

import { Root } from "./root";

import Navigation from '../pages/components/Navigation';

import Banner from '../pages/components/Banner';




const router = createBrowserRouter([
  {
    path: '/',
    element: <Root />,
    children: [
      {
        path: "/",
        element: <Home />
      },
      {
        path: "navigation",
        element: <Navigation />
      },
      {
        path: "banner",
        element: <Banner />
      },
      {
        path: "/patient",
        element: <Patient />
      },
      {
        path: "/aboutus",
        element: <AboutUs />
      },
      {
        path: "/contact",
        element: <Contact />
      },
    ]
  }
]);

export default router;
