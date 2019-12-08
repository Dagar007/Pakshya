import React, { useContext, useEffect } from "react";
import { Grid } from "semantic-ui-react";
import PostList from "./PostList";
import { observer } from "mobx-react-lite";
import PostStore from "../../../app/stores/postStore";
import LoadingComponent from "../../../app/layout/LoadingComponent";

const PostDashboard: React.FC = () => {

  const postStore = useContext(PostStore);

  useEffect(() => {
    postStore.loadPosts();
  }, [postStore]);

  if (postStore.loadingInitial)
    return <LoadingComponent content='Loading your posts' />;


  return (
    <Grid>
      <Grid.Column width={10}>
        <PostList />
      </Grid.Column>
      <Grid.Column width={6}>
      <h2>Post Filters</h2>
      </Grid.Column>
    </Grid>
  );
};

export default observer(PostDashboard);
