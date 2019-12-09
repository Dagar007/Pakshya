import React, { useContext, useEffect } from "react";
import {  Grid } from "semantic-ui-react";
import { observer } from "mobx-react-lite";
import PostStore from "../../../app/stores/postStore";
import { RouteComponentProps } from "react-router";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import PostDetailedHeader from "./PostDetailedHeader";
import PostDetailedChat from "./PostDetailedChat";
import PostDetailedInfo from "./PostDetailedInfo";
import PostDetailedSidebar from "./PostDetailedSidebar";

interface DetailParams {
  id: string;
}

const PostDetails: React.FC<RouteComponentProps<DetailParams>> = ({
  match
}) => {
  const postStore = useContext(PostStore);
  const {
    post,
    loadPost,
    loadingInitial
  } = postStore;

  useEffect(() => {
    loadPost(match.params.id);
  }, [loadPost, match.params.id]);

  if (loadingInitial || !post)
    return <LoadingComponent content='Loading Post..' />;
  return (
   <Grid>
     <Grid.Column width={10}>
        <PostDetailedHeader />
        <PostDetailedInfo />
        <PostDetailedChat />
     </Grid.Column>
     <Grid.Column width={6}>
        <PostDetailedSidebar />
     </Grid.Column>
   </Grid>
  );
};

export default observer(PostDetails);
