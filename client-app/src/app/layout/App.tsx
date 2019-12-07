import React, { useEffect, Fragment, useContext } from "react";
import { Container } from "semantic-ui-react";
import NavBar from "../../features/nav/NavBar";
import PostDashboard from "../../features/posts/dashboard/PostDashboard";
import LoadingComponent from "./LoadingComponent";
import PostStore  from "../stores/postStore"
import { observer } from "mobx-react-lite";

const App = () => {
  const postStore = useContext(PostStore)

  useEffect(() => {
    postStore.loadPosts()
  }, [postStore]);

  if (postStore.loadingInitial) return <LoadingComponent content='Loading your posts' />;

  return (
    <Fragment>
      <NavBar />
      <Container style={{ marginTop: "7em" }}>
        <PostDashboard />
      </Container>
    </Fragment>
  );
};

export default observer(App);
