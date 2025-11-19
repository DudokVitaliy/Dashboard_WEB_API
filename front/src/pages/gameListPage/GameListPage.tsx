import { useGetGamesQuery } from '../../store/apis/gameApi';
import { Box, Grid, LinearProgress } from '@mui/material';
import GameCard from '../../cards/GameCard';



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
        </Grid>
  ) 
}
export default GameListPage;