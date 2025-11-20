import { useGetGamesQuery } from '../../store/apis/gameApi';
import { Box, Grid, IconButton, LinearProgress } from '@mui/material';
import GameCard from '../../cards/GameCard';
import AddIcon from "@mui/icons-material/Add"
import { Link } from 'react-router';
import { useAppSelector } from "../../hooks/reduxHooks";



const GameListPage = () => {
    const {data, isLoading} = useGetGamesQuery(null);
    const { user } = useAppSelector((state) => state.auth);

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
                {user && user.roles.includes("admin") && (
                    <Link to="create">
                        <IconButton
                            color="secondary"
                            aria-label="Add new game"
                            size="large"
                        >
                            <AddIcon sx={{ fontSize: "3em" }} />
                        </IconButton>
                    </Link>
                )}
            </Grid>
        </Grid>
    );
};

export default GameListPage;