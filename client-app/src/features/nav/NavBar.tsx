import React from "react";
import { Menu, Container, Button } from "semantic-ui-react";
import { observer } from "mobx-react-lite";
import {  NavLink } from "react-router-dom";

const NavBar: React.FC = () => { 
  return (
    <Menu fixed='top' inverted>
      <Container>
        <Menu.Item header as={NavLink} exact to='/'>
          <img src="/assests/logo.png" alt="logo" style={{marginRight:10}}/>
          Pakshya
          </Menu.Item>
        <Menu.Item name='Posts' as={NavLink} to='/posts' />
        <Menu.Item >
          <Button as={NavLink} to='/createPost' positive content='Create Post'/>
        </Menu.Item>
      </Container>
    </Menu>
  );
};

export default observer(NavBar);
