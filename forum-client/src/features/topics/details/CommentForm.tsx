import { Field, FieldProps, Form, Formik } from "formik";
import { observer } from "mobx-react-lite";
import React from "react";
import { Loader } from "semantic-ui-react";
import * as Yup from 'yup'
import { ChatComment } from "../../../app/models/comment";
import { useStore } from "../../../app/stores/store";

interface Props{
    commentBody:string

}
export default observer( function CommentForm({commentBody} : Props){

    const {commentStore} = useStore();
    const{editMode, selectedComment, setEditMode, setSelectedComment} = commentStore;

   
    return(



        <Formik
        onSubmit={(values, { resetForm }) =>{
            if(!editMode)
                commentStore.addComent(values).then(() => resetForm())
            else{
                setEditMode(false);
                commentStore.updateComment(values).then(() => resetForm())
            }
        }}
        initialValues={{ body: commentBody}}
        validationSchema={Yup.object({
            body: Yup.string().required()
        })}
    >
        {({ isSubmitting, isValid, handleSubmit }) => (
            <Form className='ui form'>
                <Field name='body'>
                    {(props: FieldProps) => (
                        <div style={{ position: 'relative' }}>
                            <Loader active={isSubmitting} />
                            <textarea
                                placeholder='Enter your comment (Enter to submit, SHIFT + enter for new line'
                                rows={2}
                                {...props.field}
                                onKeyPress={e => {
                                  
                                    if (e.key === 'Enter' && e.shiftKey)
                                        return;
                                    if (e.key === 'Enter' && !e.shiftKey) {
                                        e.preventDefault();
                                        isValid && handleSubmit();
                                    }
                                } } />
                        </div>
                    )}
                </Field>
            </Form>
        )}

    </Formik>
    );
})