import { observer } from "mobx-react-lite";
import React from "react";
import { Link, NavLink } from "react-router-dom";
import { Button, Container, Menu, Image, Dropdown } from "semantic-ui-react";
import { useStore } from "../stores/store";


export default observer(function NavBar() {
    const { userStore: { user, logout, isAdmin } } = useStore();
    return (
        <Menu inverted fixed='top'>
            <Container>
                <Menu.Item as={NavLink} to='/' exact header>
                    <img src="/assets/logo.png" alt="logo" style={{ marginRight: 10 }} />
                    Guitar Forum
                </Menu.Item>
                <Menu.Item as={NavLink} to='/topics' name='Topics' />
                <Menu.Item>
                    <Button as={NavLink} to='/createTopic' positive content='Create Topic' />
                </Menu.Item>

                {isAdmin && (
                    <>
                        <Menu.Item as={NavLink} to='/categories' content='Categories' />
                    </>
                )}
                <Menu.Item position='right'>
                    <Image src={user?.image  || '/assets/user.png'} avatar spaced='right' />
                    <Dropdown pointing='top right' text={user?.displayName}>
                        <Dropdown.Menu>
                            <Dropdown.Item as={Link} to={`/profiles/${user?.username}`} text='MyProfile' icon='user' />
                            <Dropdown.Item onClick={logout} icon='power' />
                        </Dropdown.Menu>
                    </Dropdown>
                </Menu.Item>
            </Container>
        </Menu>
    )
})