import React from 'react';
import Navigation from '../pages/components/Navigation'
import Header from '../pages/components/Header'
import Feature from '../pages/components/Feature'
import Companies from '../pages/components/Companies'
import Subscription from '../pages/components/Subscription'


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