import { ErrorMessage, Formik } from "formik";
import { observer } from "mobx-react-lite";
import React from "react";
import { useHistory } from "react-router";
import { Button, Form, Label } from "semantic-ui-react";
import MyTextInput from "../../app/common/MyTextInput";
import { useStore } from "../../app/stores/store";

export default observer( function LoginForm() {
    const {userStore} = useStore();
    const history = useHistory();
    return(
        <Formik
        initialValues={{email: '', password: '', error: null}}
        onSubmit={(values,{setErrors}) =>{
        userStore.login(values).catch(error => setErrors({error: 'Invalid email or password'})).then(() => history.push("/topics"))
       
    
    }}
        >
            {({handleSubmit, isSubmitting, errors}) => (
                <Form className='ui form' onSubmit={handleSubmit} autoComplete='off'>
                    <MyTextInput name='email' placeholder='Email' />
                    <MyTextInput name='password' placeholder='Password' type='password' />
                    <ErrorMessage
                    name='error' render={() => (<Label style={{maginBottom:10}} basic color='red' content={errors.error} />)}/>
                    <Button loading={isSubmitting} positive content='Login' type='submit' fluid/>
                </Form>
            )}
        </Formik>
    )
}
)