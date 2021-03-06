
import { ErrorMessage, Form, Formik } from "formik";
import { observer } from "mobx-react-lite";
import React from "react";
import { Button, Header } from "semantic-ui-react";
import { useStore } from "../../app/stores/store";
import * as Yup from 'yup'
import MyTextInput from "../../app/common/MyTextInput";
import ValidationErrors from "../errors/ValidationErrors";
import MyTextArea from "../../app/common/MyTextArea";
import { useHistory } from "react-router";


export default observer(function RegisterForm(){

    const {userStore} = useStore();
    const history = useHistory();
    return(
        <Formik
        initialValues={{displayName: '', username: '',email: '',bio:'', password: '', error: null}}
        onSubmit={(values, {setErrors}) => userStore.register(values).catch(error =>
             setErrors({error}))}
             validationSchema={Yup.object({
                 displayName: Yup.string().required(),
                 username: Yup.string().required(),
                 bio: Yup.string().required(),
                 email: Yup.string().required().email(),
                 password: Yup.string().required(),
             })}
        >
            {({handleSubmit, isSubmitting, errors, isValid, dirty}) => (
                <Form className='ui form error' onSubmit={handleSubmit} autoComplete='off'>
                    <Header as='h2' content='Sign upReactivities' color='teal' textAlign='center' />                   
                    <MyTextInput name='displayName' placeholder='Display Name'  />
                    <MyTextInput name='username' placeholder='Username'  />
                    <MyTextArea name='bio' placeholder='Bio' rows={3} />
                    <MyTextInput name='email' placeholder='Email' />
                    <MyTextInput name='password' placeholder='Password' type='password' />
                    <ErrorMessage 
                    name='error' render={() => 
                    <ValidationErrors errors={errors.error}/>} />

                   
                    <Button disabled={!isValid || !dirty || isSubmitting}
                     loading={isSubmitting} positive content='Register' type='submit' fluid />
                </Form>
                
            )}
        </Formik>
    );
})