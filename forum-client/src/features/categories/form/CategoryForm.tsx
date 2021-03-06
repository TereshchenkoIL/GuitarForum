import {ErrorMessage, Form, Formik } from "formik";
import { observer } from "mobx-react-lite";
import React, { useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import { Button,  Header, Segment } from "semantic-ui-react";
import * as Yup from 'yup'
import MyTextArea from "../../../app/common/MyTextArea";
import MySelectInput from "../../../app/common/MySelectInput";
import MyTextInput from "../../../app/common/MyTextInput";
import { useStore } from "../../../app/stores/store";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import { v4 as uuid } from 'uuid'
import Category, { CategoryValues} from "../../../app/models/category";
import ValidationErrors from "../../errors/ValidationErrors";
import { toast } from "react-toastify";

export default observer(function TopicForm(){

    const{categoryStore:{createCategory, updateCategory, loadById, selectedCategory} } = useStore();

    const { id } = useParams<{ id: string }>();

    const validationScheme = Yup.object({
        name: Yup.string().required('Category name is required'),
    
    });

    const [category, setCategory] = useState<Category>(new CategoryValues())



    useEffect(() => {
        if (id) loadById(id).then((category) => setCategory(new CategoryValues(selectedCategory!)));

    }, [id, loadById, selectedCategory]);

 function handleFormSubmit(values: Category, setSubmitting: (state: boolean) =>void){
        
       
        if (!category.id) {
            let newCategory = {
                ...values,
                id: uuid()
            }
            
             createCategory(newCategory).catch(error =>
                toast.error(error.toString()));
                setSubmitting(false);
        } else {
            console.log(values);
             updateCategory(values).catch(error =>
                toast.error(error.toString()));
                setSubmitting(false);
        }
    }

    //if ( categoryStore.loadingInitial) return <LoadingComponent content='Loading categories...' />
    return (
        <Segment clearing>
            <Header content='Category Details' sub color='teal' />
            <Formik
                validationSchema={validationScheme}
                enableReinitialize
                initialValues={{...category,  error: null}}
                onSubmit={(values, {setErrors, setSubmitting}) => handleFormSubmit(values,setSubmitting)}
            >
                {({ handleSubmit, isValid, isSubmitting, dirty, errors }) => (
                    <Form className='ui form' onSubmit={handleSubmit} autoComplete='off'>
                        <MyTextInput name='name' placeholder='Name' />
                        <ErrorMessage 
                            name='error' render={() => 
                            <ValidationErrors errors={errors}/>} />
                        <Button
                            disabled={isSubmitting || !dirty || !isValid}
                            loading={isSubmitting}
                            floated='right'
                            positive type='submit'
                            content='Submit' />
                        <Button as={Link} to='/categories' floated='right' type='button' content='Cancel' />
                    </Form>

                )}
            </Formik>

        </Segment>

    )
}
);