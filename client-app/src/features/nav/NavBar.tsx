import React, { useContext } from "react";
import { Menu, Container, Button } from "semantic-ui-react";
import { observer } from "mobx-react-lite";
import PostStore from "../../app/stores/postStore";

const NavBar: React.FC = () => {
  const postStore = useContext(PostStore);
 
  return (
    <Menu fixed='top' inverted>
      <Container>
        <Menu.Item header>
          <img src="/assests/logo.png" alt="logo" style={{marginRight:10}}/>
          Pakshya
          </Menu.Item>
        <Menu.Item name='Posts' />
        <Menu.Item >
          <Button onClick={postStore.openCreateForm} positive content='Create Post'/>
        </Menu.Item>
      </Container>
    </Menu>
  );
};

export default observer(NavBar);
