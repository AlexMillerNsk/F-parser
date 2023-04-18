module Types

type Message =
    { Id: int
      Text: string }

type Root =
    { Id: int
      Name: string
      Type: string
      Messages: Message }