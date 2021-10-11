import React from 'react';
import { Route } from 'react-router';
import { Container } from 'semantic-ui-react';
import HomePage from '../../features/home/HomePage';
import ProfilePage from '../../features/profile/ProfilePage';
import TopicDashboard from '../../features/topics/dashboard/TopicDashboard';
import TopicDetail from '../../features/topics/details/TopicDetail';
import TopicForm from '../../features/topics/form/TopicForm';
import LoginForm from '../../features/users/LoginForm';
import './App.css';
import Navbar from './Navbar';

function App() {
  return (
    <>
      <Navbar />
      <Container style={{ marginTop: '7em' }}>
        <Route path='/' exact component={LoginForm} /> 
        <Route path='/topics' exact component={TopicDashboard} /> 
        <Route path='/createTopic' exact component={TopicForm} /> 
        <Route path='/editTopic/:id' exact component={TopicForm} /> 
        <Route path='/topics/:id' exact component={TopicDetail} /> 
        <Route path='/profiles/:username' exact component={ProfilePage} />
      </Container>
    </>
  );
}

export default App;
