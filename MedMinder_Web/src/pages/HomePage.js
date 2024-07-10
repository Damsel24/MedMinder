import React from 'react';
import Navigation from './components/Navigation'
import Feature from './components/Feature'
import Header from './components/Header'
import Subscription from './components/Subscription'
import Companies from './components/Companies'


export default function HomePage(){
    return (
        <div>
     
      <Navigation />
      <Header />
      <Feature />
      <Companies />
      <Subscription />
    </div>

    );

}