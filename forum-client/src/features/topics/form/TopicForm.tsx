import { Formik } from "formik";
import { observer } from "mobx-react-lite";
import React, { useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import { Button, Form, Header, Segment } from "semantic-ui-react";
import * as Yup from 'yup'
import MyTextArea from "../../../app/common/MyTextArea";
import MySelectInput from "../../../app/common/MySelectInput";
import MyTextInput from "../../../app/common/MyTextInput";
import { TopicFormValues } from "../../../app/models/topic";
import { useStore } from "../../../app/stores/store";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import { v4 as uuid } from 'uuid'

export default observer(function TopicForm(){

    const{categoryStore, topicStore, userStore} = useStore();
    const { createTopic, updateTopic, loadTopic, loadingInitial } = topicStore;
    const [topic, setTopic] = useState<TopicFormValues>(new TopicFormValues());
    const [categoryOptions, setCategoryOptions] = useState([{}]);
    const { id } = useParams<{ id: string }>();

    const validationScheme = Yup.object({
        title: Yup.string().required('The Topic title is required'),
        body: Yup.string().required('The Topic description is required'),
        category: Yup.object().required('The Topic category is required'),
    });



    useEffect(() => {
       categoryStore.loadCategories();

       const categories = categoryStore.Categories.map(item => ({key: item.id, value: item, text: item.name}));
       setCategoryOptions(categories);

       if(id) loadTopic(id).then(topic => setTopic(new TopicFormValues(topicStore.selectedTopic!)))

    }, [categoryStore, id, loadTopic])

    function handleFormSubmit(values: TopicFormValues){
        
       
        console.log(topic)
        if (!topic.id) {
            let newTopic = {
                ...topic,
                id: uuid()
            }
            newTopic.body = values.body;
            newTopic.title = values.title;
            newTopic.category = values.category;
            
            createTopic(newTopic);
        } else {
            console.log(values);
            updateTopic(values);
        }
    }

    if (topicStore.loadingInitial || categoryStore.loadingInitial) return <LoadingComponent content='Loading topic...' />
    return (
        <Segment clearing>
            <Header content='Topic Details' sub color='teal' />
            <Formik
                validationSchema={validationScheme}
                enableReinitialize
                initialValues={topic}
                onSubmit={values => handleFormSubmit(values)}
            >
                {({ handleSubmit, isValid, isSubmitting, dirty }) => (
                    <Form className='ui form' onSubmit={handleSubmit} autoComplete='off'>
                        <MyTextInput name='title' placeholder='title' />
                        <MyTextArea rows={3} placeholder='Body' name='body' />
                        <MySelectInput options={categoryOptions} placeholder='Category' name='category' />
                     
                        <Button
                            disabled={isSubmitting || !dirty || !isValid}
                            loading={isSubmitting}
                            floated='right'
                            positive type='submit'
                            content='Submit' />
                        <Button as={Link} to='/activities' floated='right' type='button' content='Cancel' />
                    </Form>

                )}
            </Formik>

        </Segment>

    )
}
);