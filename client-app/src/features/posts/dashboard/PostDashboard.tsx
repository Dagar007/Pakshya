import React, { useContext } from "react";
import { Grid } from "semantic-ui-react";
import PostList from "./PostList";
import PostDetails from "../details/PostDetails";
import PostForm from "../form/PostForm";
import { observer } from "mobx-react-lite";
import PostStore from "../../../app/stores/postStore";

const PostDashboard: React.FC = () => {
  const postStore = useContext(PostStore);
  const {editMode, selectedPost} = postStore;
  return ( 
    <Grid>
      <Grid.Column width={10}>
        <PostList/>
      </Grid.Column>
      <Grid.Column width={6}>
        {selectedPost && !editMode && (
          <PostDetails/>
        )}
        {editMode && (
          <PostForm
            key={(selectedPost && selectedPost.id) || 0}
            post={selectedPost}
          />
        )}
      </Grid.Column>
    </Grid>
  );
};

export default observer(PostDashboard);
