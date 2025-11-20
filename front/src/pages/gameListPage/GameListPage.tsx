import { useGetGamesQuery } from '../../store/apis/gameApi';
import { Box, Grid, IconButton, LinearProgress } from '@mui/material';
import GameCard from '../../cards/GameCard';
import AddIcon from "@mui/icons-material/Add"
import { Link } from 'react-router';



const GameListPage = () => {
    const {data, isLoading} = useGetGamesQuery(null);

    if(isLoading){
        return (
            <Box>
                <LinearProgress color='secondary' />
            </Box>
        )
    }
    return(
        <Grid container spacing={2} mt={2}>
            {data?.data?.map((game) => (
                <Grid size={3} key={game.id}>
                    <GameCard game={game} />
                </Grid>
            ))}
            <Grid size = {3} key = {"addGame"} display={"flex"} alignItems={"center"} justifyContent={"center"}>
                <Link to = "create">
                    <IconButton  color='secondary' aria-label='add new game' size = 'large'>
                        <AddIcon fontSize='large' sx = {{fontSize: "3em"}}/>
                    </IconButton>
                </Link>
                
            </Grid>
        </Grid>
  ) 
}
export default GameListPage;