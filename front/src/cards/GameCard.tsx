import React from "react";
import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import CardMedia from "@mui/material/CardMedia";
import Typography from "@mui/material/Typography";
import Button from "@mui/material/Button";
import CardActionArea from "@mui/material/CardActionArea";
import CardActions from "@mui/material/CardActions";
import type { GameCardProps } from "./types";
import { imagesUrl } from "../env";
import { Link } from "react-router";

const GameCard: React.FC<GameCardProps> = ({ game }) => {
    return (
        <Card sx={{ maxWidth: 345, height: "100%" }}>
            <CardActionArea>
                <CardMedia
                    component="img"
                    height="140"
                    image={`${imagesUrl}/${
                        game.mainImage
                            ? game.mainImage.imagePath
                            : "default.png"
                    }`}
                    alt="green iguana"
                    
                />
                <CardContent>
                    <Typography gutterBottom variant="h5" component="div">
                        {game.name}
                    </Typography>
                    <Typography
                        variant="body2"
                        sx={{ color: "text.secondary" }}
                    >
                        {game.price}₴
                    </Typography>
                </CardContent>
            </CardActionArea>
            <CardActions>
                <Link to = {`/games/${game.id}`} style={{ textDecoration: 'none' }}>
                    <Button size="small" color="primary">
                        Детальніше
                    </Button>
                </Link>
                
            </CardActions>
        </Card>
    );
};

export default GameCard;