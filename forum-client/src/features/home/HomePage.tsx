import React from "react";
import { Link } from "react-router-dom";
import { Container, Header, Segment, Image, Button } from "semantic-ui-react";
import { useStore } from "../../app/stores/store";


export default function HomePage(){

    const {userStore:{isLoggedIn}} = useStore()
    return(
        <Segment  textAlign='center' vertical >
            <Header as="h1" >
                <Image size='massive' src='/assets/logo.png' alt='logo' style={{ marginBottom: 12 }} />
               Guitar Forum
            </Header>
            {isLoggedIn ? (
                <>
                    <Header as="h2"  cpntent='Welcome to Guitar Forum' />
                    <Button as={Link} to='/topics' size='huge' >
                        Go to Topics
                    </Button>
                </>
            ) :
                (
                    <>
                        <Button as={Link} to='/login'  size='huge' >
                            LogIn
                        </Button>
                        <Button as={Link} to='/register' size='huge' >
                            Register
                        </Button>
                    </>
                )}

        </Segment>

    );
}