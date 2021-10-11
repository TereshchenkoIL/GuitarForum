import React from "react"
import { ErrorMessage, Form, Formik } from "formik";

import * as Yup from 'yup'
import ValidationErrors from "../errors/ValidationErrors";

import { useStore } from "../../app/stores/store";
import { Button } from "semantic-ui-react";
import MyTextInput from "../../app/common/MyTextInput";
import MyTextArea from "../../app/common/MyTextArea";

interface Props {
    updateProdileMode: (value: boolean) => void
}

export default function ProfileUpdateForm({ updateProdileMode }: Props) {

    const { profileStore } = useStore();
    return (<Formik
        initialValues={{ displayName: profileStore.profile!.displayName, bio: profileStore.profile!.bio, error: null }}
        onSubmit={(values, { setErrors }) => {
            profileStore.updateProfile(values).then(() => updateProdileMode(false)).catch(error =>
                setErrors({ error }))
        }

        }
        validationSchema={Yup.object({
            displayName: Yup.string().required(),
            bio:  Yup.string().required()
        })}
    >
        {({ handleSubmit, isSubmitting, errors, isValid, dirty }) => (
            <Form className='ui form error' onSubmit={handleSubmit} autoComplete='off'>

                <MyTextInput name='displayName' placeholder='Display Name' />
                <MyTextArea name='bio' placeholder='Bio' rows={5} />
                <ErrorMessage
                    name='error' render={() =>
                        <ValidationErrors errors={errors.error} />}

                />
                <Button disabled={!isValid || !dirty || isSubmitting}
                    loading={isSubmitting} positive content='Update' type='submit' fluid />
            </Form>
        )}
    </Formik>)
}