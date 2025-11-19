import {createApi, fetchBaseQuery} from "@reduxjs/toolkit/query/react";
import { apiBaseUrl } from "../../env";
import type { ServiceResponse } from "../../pages/loginPage/services/types";
import type { Game } from "../../pages/gameListPage/types";

const gameApi = createApi({
    reducerPath: 'game',
    baseQuery: fetchBaseQuery({
        baseUrl: `${apiBaseUrl}/Game`
    }),
    tagTypes: ['Games'],
    endpoints: (build) => ({
        getGames: build.query<ServiceResponse<Game[]>, null>({
            query: () => ({
                url: '/all',
                method: 'get'
        }),
        providesTags: ['Games'],
        }),
        getGameById: build.query<ServiceResponse<Game>, string>({
            query: (id) => ({
                url: `/${id}`,
                method: 'get'
            }),
            providesTags: (result, error, id) => [{ type: 'Games', id }],
        }),
    })

});

export const { useGetGamesQuery, useGetGameByIdQuery } = gameApi;
export default gameApi;