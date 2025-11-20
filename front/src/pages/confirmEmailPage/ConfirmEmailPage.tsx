import axios from "axios";
import { useEffect } from "react";
import { useSearchParams } from "react-router";
import { apiBaseUrl } from "../../env";

const ConfirmEmailPage = () => {
    const [searchParams] = useSearchParams();

    useEffect(() => {
        let url = apiBaseUrl + "/auth/confirmemail?";
        const params = [];
        for(const param of searchParams) {
            params.push(`${param[0]}=${param[1]}`)
        }
        url += params.join('&');
        axios.get(url)
        .then(response => {
            if(response.status == 200) {
                return (
                    <h1>Пошта підтверджена</h1>
                );
            }
        })
        .catch(error => {
            console.log(error);
            return (
                <h1>Помилка</h1>
            )
        });
    }, []);

    return (
        <h1>Пошта підтверджена</h1>
    )
}

export default ConfirmEmailPage;