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

if (!isObject(CenterPositionBehavior))
{
    %template = new BehaviorTemplate(CenterPositionBehavior);
}

//-----------------------------------------------------------------------------

function CenterPositionBehavior::onAddToScene(%this, %scene)
{
    // Get the number of sprites in the composite
    %count = %this.owner.getSpriteCount();
    %length = mSqrt(%count);

    %spriteSize = %this.owner.getDefaultSpriteSize();
    %boardSizeX = %length * %spriteSize.x;
    %boardSizeY = %length * %spriteSize.y;

    %x = (%boardSizeX / 2) - (%spriteSize.x / 2);
    %y = (%boardSizeY / 2) - (%spriteSize.y / 2);

    %this.owner.setPosition(-%x, -%y);
}