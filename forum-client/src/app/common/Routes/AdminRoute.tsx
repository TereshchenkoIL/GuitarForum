import { Redirect, Route, RouteComponentProps, RouteProps } from "react-router-dom";
import { useStore } from "../../stores/store";
interface Props extends RouteProps {
    component: React.ComponentType<RouteComponentProps<any>> | React.ComponentType<any>;
}

export default function AdminRoute({ component: Component, ...rest }: Props) {
    const { userStore: { isAdmin } } = useStore();
    return (
        <Route
            {...rest}
            render={(props) => isAdmin ? <Component {...props} /> : <Redirect to='/' />}
        />
    )
}