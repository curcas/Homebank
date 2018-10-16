module Main exposing (Model, Msg(..), fetchUsers, init, main, subscriptions, update, view)

import Browser
import Data.User exposing (User)
import Html exposing (Html, button, div, li, text, ul)
import Html.Events exposing (onClick)
import Http exposing (..)
import Request.Account exposing (..)


main =
    Browser.element
        { init = init
        , update = update
        , subscriptions = subscriptions
        , view = view
        }


type alias Model =
    List User


type Msg
    = Load
    | OnFetchAccounts (Result Http.Error (List User))


init : () -> ( Model, Cmd Msg )
init _ =
    ( []
    , fetchUsers
    )


update : Msg -> Model -> ( Model, Cmd Msg )
update msg model =
    case msg of
        Load ->
            ( [], fetchUsers )

        OnFetchAccounts (Ok data) ->
            ( data
            , Cmd.none
            )

        OnFetchAccounts (Err _) ->
            ( model, Cmd.none )


subscriptions : Model -> Sub Msg
subscriptions model =
    Sub.none


view : Model -> Html Msg
view model =
    div []
        [ button [ onClick Load ] [ text "Load data" ]
        , ul [] (List.map (\u -> li [] [ text u.name ]) model)
        ]


fetchUsers : Cmd Msg
fetchUsers =
    apiAccountGet |> Http.send OnFetchAccounts
