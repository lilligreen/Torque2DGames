//-----------------------------------------------------------------------------
// Copyright (c) 2013 Mike Lilligreen
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to
// deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
// sell copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
// IN THE SOFTWARE.
//-----------------------------------------------------------------------------

if (!isObject(ButtonBehavior))
{
    %template = new BehaviorTemplate(ButtonBehavior);
}

//-----------------------------------------------------------------------------

function ButtonBehavior::onTouchUp(%this, %touchID, %worldPosition)
{
    %newColor = %this.owner.getImageFrame();
    echo("test");
    TileBoard.selectSprite("0 14");
    %oldColor = TileBoard.getSpriteImageFrame();

    %stackObject = TamlRead("./../objects/Stack.taml");
    %locStack = %stackObject.getBehavior(StackBehavior);

    %locStack.push("0 14");

    %counter = 0;

    while (%stackObject.len > 0)
    {
        %topCard = %locStack.pop();
        
        TileBoard.selectSprite(%topCard);
        TileBoard.setSpriteImageFrame(%newColor);
        TileBoard.deselectSprite();

        if (%topCard.x > 0)
        {
            TileBoard.selectSprite((%topCard.x - 1) SPC %topCard.y);
            %leftColor = TileBoard.getSpriteImageFrame();
            TileBoard.deselectSprite();

            if (%leftColor == %oldColor)
            {
                %locStack.push((%topCard.x - 1) SPC %topCard.y);
            }
        }

        if (%topCard.x < 14)
        {
            TileBoard.selectSprite((%topCard.x + 1) SPC %topCard.y);
            %rightColor = TileBoard.getSpriteImageFrame();
            TileBoard.deselectSprite();

            if (%rightColor == %oldColor)
            {
                %locStack.push((%topCard.x + 1) SPC %topCard.y);
            }
        }

        if (%topCard.y > 0)
        {
            TileBoard.selectSprite(%topCard.x SPC (%topCard.y - 1));
            %upColor = TileBoard.getSpriteImageFrame();
            TileBoard.deselectSprite();

            if (%upColor == %oldColor)
            {
                %locStack.push(%topCard.x SPC (%topCard.y - 1));
            }
        }

        if (%topCard.y < 14)
        {
            TileBoard.selectSprite(%topCard.x SPC (%topCard.y + 1));
            %downColor = TileBoard.getSpriteImageFrame();
            TileBoard.deselectSprite();

            if (%downColor == %oldColor)
            {
                %locStack.push(%topCard.x SPC (%topCard.y + 1));
            }
        }

        %counter++;
    }
    
    %locStack.delete();
}