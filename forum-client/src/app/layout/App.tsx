import React from 'react';
import { Route } from 'react-router';
import { Container } from 'semantic-ui-react';
import CategoryDashboard from '../../features/categories/dashboard/CategoryDashboard';
import CategoryForm from '../../features/categories/form/CategoryForm';
import NotFound from '../../features/errors/NotFound';
import HomePage from '../../features/home/HomePage';
import ProfilePage from '../../features/profile/ProfilePage';
import TopicDashboard from '../../features/topics/dashboard/TopicDashboard';
import TopicDetail from '../../features/topics/details/TopicDetail';
import TopicForm from '../../features/topics/form/TopicForm';
import LoginForm from '../../features/users/LoginForm';
import RegisterForm from '../../features/users/RegisterForm';
import AdminRoute from '../common/Routes/AdminRoute';
import AuthorizedRoute from '../common/Routes/AuthorizedRoute';
import './App.css';
import Navbar from './Navbar';
import { ToastContainer } from 'react-toastify';

function App() {
  return (
    <>
      
      <Container style={{ marginTop: '7em' }}>
        <ToastContainer position='bottom-right' hideProgressBar />
        <Route path='/' exact component={HomePage} /> 
        <Route path='/login' exact component={LoginForm}/>
        <Route path='/register' exact component={RegisterForm}/>
        <Route path={'/(.+)'}
        render={() => (
          <>
            <Navbar />
            <AuthorizedRoute path='/topics' exact component={TopicDashboard} /> 
           
            <AuthorizedRoute path='/createTopic' exact component={TopicForm} /> 
            <AuthorizedRoute path='/editTopic/:id' exact component={TopicForm} /> 
            <AdminRoute path='/editCategory/:id' exact component={CategoryForm} /> 
            <AdminRoute path='/createCategory' exact component={CategoryForm} /> 
            <AuthorizedRoute path='/topics/:id' exact component={TopicDetail} /> 
            <AuthorizedRoute path='/profiles/:username' exact component={ProfilePage} />
            <AdminRoute path='/categories' exact component={CategoryDashboard} />
            <Route path='/notFound' component={NotFound} />           
          </> 
        )}/>
      </Container>
    </>
  );
}

export default App;
