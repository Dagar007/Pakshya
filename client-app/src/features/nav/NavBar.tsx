import React from "react";
import { Menu, Container, Button } from "semantic-ui-react";

interface IProps {
  openCreateForm: () => void;
}

const NavBar: React.FC<IProps> = ({openCreateForm}) => {
  return (
    <Menu fixed='top' inverted>
      <Container>
        <Menu.Item header>
          <img src="/assests/logo.png" alt="logo" style={{marginRight:10}}/>
          Pakshya
          </Menu.Item>
        <Menu.Item name='Posts' />
        <Menu.Item >
          <Button onClick={openCreateForm} positive content='Create Post'/>
        </Menu.Item>
      </Container>
    </Menu>
  );
};

export default NavBar;
