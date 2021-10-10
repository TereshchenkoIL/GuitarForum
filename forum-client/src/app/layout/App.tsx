import React from 'react';
import { Route } from 'react-router';
import { Container } from 'semantic-ui-react';
import HomePage from '../../features/home/HomePage';
import TopicDashboard from '../../features/topics/dashboard/TopicDashboard';
import TopicDetail from '../../features/topics/details/TopicDetail';
import LoginForm from '../../features/users/LoginForm';
import './App.css';
import Navbar from './Navbar';

function App() {
  return (
    <>
      <Navbar />
      <Container style={{ marginTop: '7em' }}>
        <Route path='/' exact component={HomePage} /> 
        <Route path='/topics' exact component={TopicDashboard} /> 
        <Route path='/createTopic' exact component={HomePage} /> 
        <Route path='/topics/:id' exact component={TopicDetail} /> 
      </Container>
    </>
  );
}

export default App;
